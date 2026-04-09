namespace FitAgenda.Domain.Models
{   
    public class TipoAula : Entity
    {
        protected TipoAula()
        {
        }

        public TipoAula(string nome, bool ativo = true)
        {
            Renomear(nome);
            DefinirStatus(ativo);
        }

        public string Nome { get; private set; } = string.Empty;
        public bool Ativo { get; private set; } = true;
        public ICollection<Aula> Aulas { get; private set; } = new List<Aula>();

        public void Renomear(string nome)
        {
            Nome = nome.Trim();
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void DefinirStatus(bool ativo)
        {
            if (ativo)
            {
                Ativar();
                return;
            }

            Desativar();
        }
    }

}
