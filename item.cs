using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
	 class item
	{
		public string name { get; set; }
		public int dmg { get; set; }
		public bool equiped = false;

		public item(string name, int dmg)
        {
			this.dmg = dmg;
			this.name = name.ToUpper();
		}
		public item(item item)
        {
			name = item.name;
			dmg = item.dmg;
        }
	}
}
