using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.ValueObjects.CEP
{
    public class CEP : ValueObject<CEP>
    {
        public CEP(string numero)
        {
            Numero = numero;
        }

        protected CEP() { }

        public string Numero { get; protected set; }
    }
}