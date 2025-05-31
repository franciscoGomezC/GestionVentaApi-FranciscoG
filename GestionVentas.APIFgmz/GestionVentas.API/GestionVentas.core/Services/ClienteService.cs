using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.DTOs;
using GestionVentas.core.Interfaces;
using GestionVentas.core.Models;

namespace GestionVentas.core.Services
{
    public class ClienteService
    {

        //la variable de lectura de IClienteRepository
        private readonly IClienteRepository _clienteRepository;

        //constructor
        public ClienteService (IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        //obtener todo de cliente
        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync(); //obtiene todos los clientes
            return clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Email = c.Email,
                Telefono = c.Telefono,
                Direccion = c.Direccion,
                FechaRegistro = c.FechaRegistro
            });
        }

        //obtener cliente por id
        public async Task<ClienteDTO> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id); //obtiene un cliente por su id
            if (cliente == null) return null; //si no existe el cliente retorna null
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                FechaRegistro = cliente.FechaRegistro
            };
        }

        //agregar cliente
        public async Task<ClienteDTO> AddAsync(ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,
                Email = clienteDTO.Email,
                Telefono = clienteDTO.Telefono,
                Direccion = clienteDTO.Direccion,
                FechaRegistro = DateTime.Now
            };
            await _clienteRepository.AddAsync(cliente); //agrega el nuevo cliente
            //retorna el cliente agregado
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                FechaRegistro = cliente.FechaRegistro
            };
        }

        //actualizar cliente
        public async Task<ClienteDTO> UpdateAsync(ClienteDTO clienteDTO)
        {
            var cliente = await _clienteRepository.GetByIdAsync(clienteDTO.Id); //obtiene el cliente por su id
            if (cliente == null) return null; //si no existe el cliente retorna null
            cliente.Nombre = clienteDTO.Nombre; //actualiza el nombre del cliente
            cliente.Apellido = clienteDTO.Apellido; //actualiza el apellido del cliente
            cliente.Email = clienteDTO.Email; //actualiza el email del cliente
            cliente.Telefono = clienteDTO.Telefono; //actualiza el telefono del cliente
            cliente.Direccion = clienteDTO.Direccion; //actualiza la direccion del cliente
            await _clienteRepository.UpdateAsync(cliente); //actualiza el cliente
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                FechaRegistro = cliente.FechaRegistro
            };
        }

        //eliminar cliente
        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id); //obtiene el cliente por su id
            if (cliente == null) return false; //si no existe el cliente retorna false
            await _clienteRepository.DeleteAsync(id); //elimina el cliente
            return true; //retorna true si se elimino el cliente
        }
    }
}
