using LaureadoPrueba.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaureadoPrueba.Dato
{
    public class PersonaClass
    {
        /// <summary>
        /// Consulta todos los datos de las personas
        /// </summary>
        /// <returns>Datos de las personas</returns>
        public List<Persona> Consultar()
        {
            using(PruebaDatabaseEntities contexto = new PruebaDatabaseEntities())
            {
                return contexto.Persona.AsNoTracking().ToList();
            }
        }
        /// <summary>
        /// Guarda los datos de una persona
        /// </summary>
        /// <param name="modelo">Datos de la persona</param>
        public void Guardar(Persona modelo)
        {
            using(PruebaDatabaseEntities contexto = new PruebaDatabaseEntities())
            {
                contexto.Persona.Add(modelo);
                contexto.SaveChanges();
            }
        }
        /// <summary>
        /// Elimina la data de la persona
        /// </summary>
        /// <param name="model">Datos de la persona</param>
        public void Eliminar(Persona modelo)
        {
            using (PruebaDatabaseEntities contexto = new PruebaDatabaseEntities())
            {
                contexto.Entry(modelo).State = System.Data.Entity.EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}
