using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardProject
{
    public class FlashCard
    {
        public int Id { get; set; }
        public Stack? Stack { get; set; }
        public string Question { get; set; } = "";
        public string Answer { get; set; } = "";
    }
}
