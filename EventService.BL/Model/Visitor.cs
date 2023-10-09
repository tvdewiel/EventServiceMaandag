using EventService.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.BL.Model
{
    public class Visitor
    {
        public Visitor(string name, int id, DateTime birthDay)
        {
            SetName(name);
            SetId(id);
            SetBirthDay(birthDay);
        }
        public Visitor(string name, DateTime birthDay)
        {
            SetName(name);
            SetBirthDay(birthDay);
        }

        public Visitor()
        {
        }

        public string Name { get; set; }
        public int Id { get;  set; }
        public DateTime BirthDay { get;  set; }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new VisitorException("SetName");
            Name = name;
        }
        public void SetId(int id)
        {
            if (id<=0) throw new VisitorException("SetId");
            Id = id;
        }
        public void SetBirthDay(DateTime birthday)
        {
            if (birthday > DateTime.Now) throw new VisitorException("SetBirthDay");
        }
    }
}
