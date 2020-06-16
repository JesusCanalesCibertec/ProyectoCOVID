using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using net.royal.spring.sistema.dominio;
using net.royal.spring.core.dominio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.planilla.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.rrhh.dominio;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.salud.dominio;
using net.royal.spring.dominio;
using net.royal.spring.minedu.dominio;

namespace net.royal.spring.framework.web.dao {
    public class MySqlDbContext: DbContext {


        public DbSet<MpSistema> MpSistema { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /** MINEDU - INICIO **/
            modelBuilder.Entity<MpSistema>().HasKey(c => new { c.IdSistema });

        }

        /** MINEDU - INICIO **/
        //public string ConnectionString { get; set; }

        //public MySqlDbContext(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection(ConnectionString);
        //}


        //public List<MpSistema> ListarSistemas()
        //{
        //    List<MpSistema> list = new List<MpSistema>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("SELECT id_si, codigo_si, nombre_si, siglas, estado  FROM sistema_informacion", conn);

        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new MpSistema()
        //                {
        //                    IdSistema = Convert.ToInt32(reader["id_si"]),
        //                    Codigo = reader["codigo_si"].ToString(),
        //                    Nombre = reader["nombre_si"].ToString(),
        //                    Siglas = reader["siglas"].ToString(),
        //                    Estado = Convert.ToInt32(reader["estado"])
        //                });
        //            }
        //        }
        //    }
        //    return list;
        //}
    }
}