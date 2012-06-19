namespace Geldverleih.Domain
{
    public class VerleihKondition
    {
        public decimal Zinssatz { get; private set; }

        public VerleihKondition(decimal zinssatz = (decimal)5.9)
        {
            Zinssatz = zinssatz;
        }
    }
}