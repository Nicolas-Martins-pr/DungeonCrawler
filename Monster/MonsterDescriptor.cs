using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public enum MonsterType
    {
        Bat,
        Skeleton,
        Zombie,
        Vampire
    }

     public class MonsterDescriptor
    {
        public MonsterType Type { get; private set; }
        public string Name { get; private set; }
        public int Initiative { get; private set; }
        public int AC { get; private set; }
        public string HD { get; private set; }

        private Random random;

        public MonsterDescriptor(MonsterType type, string name, int initiative, int ac, string hd)
        {
            Type = type;
            Name = name;
            Initiative = initiative;
            AC = ac;
            HD = hd;
            random = new Random();
        }

        public int ComputeHD()
        {
            int computedHD = 0;
            int[] diceAndValue = HD.Split('d').Select(Int32.Parse).ToArray();
            for(int i =0; i < diceAndValue[0]; i++)
            {
                computedHD+= random.Next(0, diceAndValue[1])+1;
            }
            return computedHD;
        }
      
    }
}
