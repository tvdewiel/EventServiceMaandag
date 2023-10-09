using EventService.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.BL.Model
{
    public class Event
    {
        private Dictionary<int, Visitor> _visitors=new Dictionary<int, Visitor>();

        public Event(string name, DateTime date, string location, int maxVisitors)
        {
            Name = name;
            Date = date;
            Location = location;
            MaxVisitors = maxVisitors;
        }

        public string Name { get;private set; }
        public DateTime Date {  get; set; }
        public string Location { get;private set; }
        public int MaxVisitors { get;private set; }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EventException("SetName");
            Name = name;
        }
        public void SetLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) throw new EventException("SetLocation");
            Location=location;
        }
        public void SetMaxVisitors(int max)
        {
            if (max <= 0) throw new EventException("SetId");
            MaxVisitors=max;
        }
        public IReadOnlyList<Visitor> Visitors => _visitors.Values.ToList().AsReadOnly();
        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("AddVisitor");
            if (_visitors.ContainsKey(visitor.Id)) throw new EventException("AddVisitor - duplicate");
            if (_visitors.Values.Count==MaxVisitors) throw new EventException("AddVisitor - max");
            _visitors.Add(visitor.Id, visitor);
        }
        public void RemoveVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("RemoveVisitor");
            if (!_visitors.ContainsKey(visitor.Id)) throw new EventException("RemoveVisitor - duplicate");     
            _visitors.Remove(visitor.Id);
        }
    }
}
