using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devoir1ClassesMetier
{
    public class Specialite
    {
        private int numSpe;
        private string nomSpe;

        public Specialite(int unNum, string unNom)
        {
            NumSpe = unNum;
            NomSpe = unNom;
        }

        public int NumSpe { get => numSpe; set => numSpe = value; }
        public string NomSpe { get => nomSpe; set => nomSpe = value; }
    }
}
