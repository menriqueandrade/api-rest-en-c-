using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select id as ""id"",
                        name as ""name"",
                        description as ""description"",
                        reference as ""reference"",
                        status as ""status"",
                        inventory as ""inventory"",
                        warehouses as ""warehouses""
              
                from product
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }




        [HttpPost]
        public JsonResult Post(Products prod)
        {
            string query = @"
                insert into product(name,description,reference,status,inventory,warehouses)
                values (@name,@description,@reference,@status,@inventory,@warehouses)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", prod.id);
                    myCommand.Parameters.AddWithValue("@name", prod.name);
                    myCommand.Parameters.AddWithValue("@description", prod.description);
                    myCommand.Parameters.AddWithValue("@reference", prod.reference);
                    myCommand.Parameters.AddWithValue("@status", prod.status);
                    myCommand.Parameters.AddWithValue("@inventory", prod.inventory);
                    myCommand.Parameters.AddWithValue("@warehouses", prod.warehouses);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Added Successfully");
        }





        [HttpPut]
        public JsonResult Put(Products prod)
        {
            string query = @"
                update product
                set name = @name,
                description  = @description,
                reference  = @reference,
                status  = @status,
                inventory  = @inventory,
                warehouses  = @warehouses
                where id=@id 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", prod.id);
                    myCommand.Parameters.AddWithValue("@name", prod.name);
                    myCommand.Parameters.AddWithValue("@description", prod.description);
                    myCommand.Parameters.AddWithValue("@reference", prod.reference);
                    myCommand.Parameters.AddWithValue("@status", prod.status);
                    myCommand.Parameters.AddWithValue("@inventory", prod.inventory);
                    myCommand.Parameters.AddWithValue("@warehouses", prod.warehouses);
                   
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }




        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from product
                where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }

    



}
