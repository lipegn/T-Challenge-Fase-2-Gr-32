namespace Core.Entity
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public required int DDD { get; set; }
        public int Telefone { get; set; }
        public string Email { get; set; }
    }
}
