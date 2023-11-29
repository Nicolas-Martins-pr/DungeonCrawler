using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Bestiary
    {
        public List<MonsterDescriptor> MonsterList { get; set; }
        public Bestiary() { 
        
            MonsterList = new List<MonsterDescriptor>();

        

        }

        public void MonsterLoader()
        {

        }
    }
}
