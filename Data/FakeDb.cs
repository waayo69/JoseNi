using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JoseNi.MVVM.Models;
using System.Threading.Tasks;

namespace JoseNi.Data
{
    internal class FakeDb
    {
        public static List<User> Users { get; set; } = new List<User>();

    }
}
