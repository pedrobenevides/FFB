using System.Collections.Generic;

namespace ProjetoCompiladores.Principal.Interfaces
{
    public interface ILeitor
    {
        IList<Elemento> LerStrings();
        IList<Elemento> LerChars();
    }
}
