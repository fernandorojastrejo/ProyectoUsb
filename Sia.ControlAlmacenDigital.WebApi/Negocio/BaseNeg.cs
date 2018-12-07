using Sia.ControlAlmacenDigital.Entidades;
using System.Collections.Generic;

namespace Sia.ControlAlmacenDigital.Negocio
{
    public abstract class BaseNeg <Tipo>
    {
        public abstract List<Tipo> Obtener();
        public abstract Tipo Obtener(int id);
        public abstract List<Tipo> Obtener(int id, string anioId);

        public abstract Resultado<Tipo> Agregar(Tipo modelo);
        public abstract Resultado<Tipo> Editar(Tipo modelo);
        public abstract Resultado<Tipo> Eliminar(int id);
    }
}