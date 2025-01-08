using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace Huffman

{

    public class HuffmanNode

    {

        public char Character { get; set; }

        public int Frequency { get; set; }



        public HuffmanNode Left { get; set; }



        public HuffmanNode Right { get; set; }



        public HuffmanNode(char character, int frequency)

        {

            Character = character;

            Frequency = frequency;

            Left = Right = null;

        }

    }

}
