using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardProject
{
    public class Stack
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<FlashCard>? FlashCards { get; set; }
    }
}
