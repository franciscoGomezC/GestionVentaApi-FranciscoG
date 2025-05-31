using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.DTOs;
using GestionVentas.core.Interfaces;
using GestionVentas.core.Models;

namespace GestionVentas.core.Services
{
    public class VentaService
    {

        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;

        public VentaService(IVentaRepository ventaRepository, IProductoRepository productoRepository)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
        }

        //obtener todas las ventas
        public async Task<IEnumerable<VentaDTO>> GetAllAsync()
        {
            var ventas = await _ventaRepository.GetAllAsync();
            return ventas.Select(ventas => new VentaDTO
            {
                Id = ventas.Id,
                Fecha = ventas.Fecha,
                Total = ventas.Total,
                ClienteId = ventas.ClienteId, //esto se añado para poder obtener el id del cliente
                ClienteNombre = $"{ventas.Cliente.Nombre} {ventas.Cliente.Apellido}",
                MetodoPago = ventas.MetodoPago,
                DetallesVenta = ventas.DetallesVenta.Select(d => new DetalleVentaDTO
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                }).ToList()
            });
        }

        //obtener una venta por id
        public async Task<VentaDTO> GetByIdAsync(int id)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);
            if (venta == null) 
                return null;

            return new VentaDTO
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                ClienteId = venta.ClienteId, //esto se añado para poder obtener el id del cliente
                ClienteNombre = $"{venta.Cliente.Nombre} {venta.Cliente.Apellido}",
                MetodoPago = venta.MetodoPago,
                DetallesVenta = venta.DetallesVenta.Select(d => new DetalleVentaDTO
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                }).ToList()
            };
        }

        //agregar una nueva venta con VentaDTO y VentaCreateDTO
        public async Task<VentaDTO> AddAsync(VentaCreateDTO ventaDto)
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                ClienteId = ventaDto.ClienteId,
                EmpleadoId = ventaDto.EmpleadoId,
                MetodoPago = ventaDto.MetodoPago,
                Total = 0,
                DetallesVenta = new List<DetalleVenta>()
            };
            foreach (var detalle in ventaDto.DetallesVenta)
            {
                var producto = await _productoRepository.GetByIdAsync(detalle.ProductoId);
                if (producto == null) continue;
                var subtotal = detalle.Cantidad * producto.Precio; // calcula el subtotal con la Cantidad del Dto y el Preicio del Modelo de Producto
                venta.DetallesVenta.Add(new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = subtotal
                });
                producto.Stock -= detalle.Cantidad; // Actualiza stock
                await _productoRepository.UpdateAsync(producto);
                venta.Total += subtotal;
            }
            await _ventaRepository.AddAsync(venta);
            return new VentaDTO
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                ClienteNombre = $"{venta.Cliente.Nombre} {venta.Cliente.Apellido}",
                MetodoPago = venta.MetodoPago,
                DetallesVenta = venta.DetallesVenta.Select(d => new DetalleVentaDTO
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,
                }).ToList()
            };
        }

        //hacer la clase para actualizar la venta
        public async Task UpdateAsync(VentaDTO ventaDTO)
        {
            var venta = await _ventaRepository.GetByIdAsync(ventaDTO.Id);
            if (venta == null) return;
            // Actualizar la venta
            venta.Fecha = ventaDTO.Fecha;
            venta.ClienteId = ventaDTO.ClienteId;
            venta.MetodoPago = ventaDTO.MetodoPago;
            venta.Total = 0;
            // Actualizar detalles de la venta
            foreach (var detalle in venta.DetallesVenta)
            {
                var producto = await _productoRepository.GetByIdAsync(detalle.ProductoId);
                if (producto == null) continue;
                var subtotal = detalle.Cantidad * producto.Precio; // calcula el subtotal con la Cantidad del Dto y el Preicio del Modelo de Producto
                detalle.Subtotal = subtotal;
                venta.Total += subtotal;
            }
            await _ventaRepository.UpdateAsync(venta);
        }

        // Eliminar una venta
        public async Task DeleteAsync(int id)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);
            if (venta == null) return;
            foreach (var detalle in venta.DetallesVenta)
            {
                var producto = await _productoRepository.GetByIdAsync(detalle.ProductoId);
                if (producto == null) continue;
                producto.Stock += detalle.Cantidad; // Actualiza stock
                await _productoRepository.UpdateAsync(producto);
            }
            await _ventaRepository.DeleteAsync(id);
        }


    }
}
