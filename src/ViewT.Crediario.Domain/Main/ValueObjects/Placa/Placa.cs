using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.ValueObjects.Placa
{
    public class Placa: ValueObject<Placa>
    {
        public string Numero { get; protected set; }

        protected Placa()
        {
            
        }

        public Placa(string numero)
        {
            Numero = numero;
        }
    }
}