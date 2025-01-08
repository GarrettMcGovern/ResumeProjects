using System.Collections.Generic;

using System.Linq;

using System.Security.AccessControl;

using System.Security.Cryptography;

using System.Text;

using System.Threading.Tasks;



namespace Huffman

{

    public class HuffmanCoding

    {

        public static void BuildFrequencyTable()

        {

            string s = File.ReadAllText("wap.txt");

            // Char is key, Freq is value  

            Dictionary<char, int> frequencyTable = new Dictionary<char, int>(); ;

            foreach (var ch in s)

            {

                if (frequencyTable.ContainsKey(ch))

                {

                    frequencyTable[ch]++;

                }

                else

                {

                    frequencyTable.Add(ch, 1);

                }



            }



            // Sorts the frequency table  

            foreach (KeyValuePair<char, int> pair in frequencyTable.OrderByDescending(value => value.Value))

            {

                Console.WriteLine("{0} {1}", pair.Key, pair.Value);

            }

            Console.WriteLine("The amount of items in this table are " + frequencyTable.Count);

        }





        public static HuffmanNode BuildTree(Dictionary<char, int> frequencyTable)

        {

            // Crate a priority Queue 

            var priorityQueue = new PriorityQueue<HuffmanNode, int>();



            // Enqueue all nodes and frequencies 

            foreach (var item in frequencyTable)

            {

                priorityQueue.Enqueue(new HuffmanNode(item.Key, item.Value), item.Value);

            }



            // Builds the tree  

            while (priorityQueue.Count > 1)

            {

                // Deque the nodes with the lowest frequencies  

                var left = priorityQueue.Dequeue();

                var right = priorityQueue.Dequeue();



                // Combines them into a new node  

                var internalNode = new HuffmanNode('\0', left.Frequency + right.Frequency)

                {

                    Left = left,

                    Right = right,

                };



                // Enqueue the new internal Node 

                priorityQueue.Enqueue(internalNode, internalNode.Frequency);

            }





            // Final node left is root  

            return priorityQueue.Dequeue();

        }



        public static void GenerateCodes(HuffmanNode node, string code, Dictionary<char, string> huffmanCodes)

        {

            // Traversing the tree 

            // Once there are no more codes to generate and add to the tree  

            if (node == null)

                return;



            // If it gets to a leaf node, add a character and it's code  

            if (node.Left == null && node.Right == null)

            {

                huffmanCodes[node.Character] = code;

                Console.WriteLine($"{node.Character}: {code}");

            }



            GenerateCodes(node.Left, code + "0", huffmanCodes);

            GenerateCodes(node.Right, code + "1", huffmanCodes);

        }



        // Asigns codes to characters based on tree structure and creates the code for each character in wap.txt 

        public static string Compress(string input, Dictionary<char, string> huffmanCodes)

        {

            StringBuilder encodedString = new StringBuilder();

            foreach (var ch in input)

            {

                encodedString.Append(huffmanCodes[ch]);

            }

            return encodedString.ToString();

        }



        // Writes those stored codes for each character to file  

        public static void WriteToFile(string filePath, string compressedText)

        {

            File.WriteAllText(filePath, compressedText);

        }



        // Takes binary from compressed file and turns it back into characters by refering back to the codes 

        public static string Decompress(string compressedText, HuffmanNode root)

        {

            StringBuilder decodedString = new StringBuilder();

            var currentNode = root;

            foreach (var bit in compressedText)

            {

                if (bit == '0')

                {

                    currentNode = currentNode.Left;

                }

                else

                {

                    currentNode = currentNode.Right;

                }



                // If a leaf node is reach, append and reset at the root  

                if (currentNode.Left == null && currentNode.Right == null)

                {

                    decodedString.Append(currentNode.Character);

                    currentNode = root;

                }

            }

            return decodedString.ToString();

        }



        // Reads from the compressed data to decompress 

        public static string ReadFromFile(string filePath)

        {

            return File.ReadAllText(filePath);

        }

    }

}