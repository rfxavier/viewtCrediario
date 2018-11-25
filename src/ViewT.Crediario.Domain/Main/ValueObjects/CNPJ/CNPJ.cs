using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.ValueObjects.CNPJ
{
    public class CNPJ: ValueObject<CNPJ>
    {
        public string Numero { get; private set; }

        protected CNPJ()
        {

        }

        public CNPJ(string numero)
        {
            Numero = numero;
        }
    }
}