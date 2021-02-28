using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;
using System.Data.SqlClient;
namespace WebApplication2.Controllers
{
    public class StudentController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Address> addresses = new List<Address>();
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
            con.ConnectionString = WebApplication2.Properties.Resources.ConnectionString;
        }

        public IActionResult Data()
        {
            FetchData();
            return View(addresses);
        }
        private void FetchData()
        {
            if (addresses.Count > 0)
            {
                addresses.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Id] ,[FullName] ,[Gender] ,[AdmissionNumber] ,[Gmail] ,[PhoneNumber] FROM[StudentsDB].[dbo].[Students]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    addresses.Add(new Address()
                    {
                        Id = dr["Id"].ToString()
                    ,
                        FullName = dr["FullName"].ToString()
                    ,
                        Gender = dr["Gender"].ToString()
                    ,
                        AdmissionNumber = dr["AdmissionNumber"].ToString()
                    ,
                        Gmail = dr["Gmail"].ToString()
                    ,
                        PhoneNumber = dr["PhoneNumber"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
