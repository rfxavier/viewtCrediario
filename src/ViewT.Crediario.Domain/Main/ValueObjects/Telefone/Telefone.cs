using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.ValueObjects.Telefone
{
    public class Telefone : ValueObject<Telefone>
    {
        public Telefone(string numero)
        {
            Numero = numero;
        }

        protected Telefone() { }
        public string Numero { get; protected set; }
    }
}