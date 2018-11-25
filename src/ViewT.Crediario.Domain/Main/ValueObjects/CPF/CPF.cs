using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.ValueObjects.CPF
{
    public class CPF: ValueObject<CPF>
    {
        public string Numero { get; private set; }

        protected CPF()
        {
            
        }

        public CPF(string numero)
        {
            Numero = numero;
        }
    }
}