using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.ValueObjects.Email
{
    public class Email : ValueObject<Email>
    {
        public string Endereco { get; protected set; }

        protected Email() { }

        public Email(string endereco)
        {
            this.Endereco = endereco;
        }
    }
}
