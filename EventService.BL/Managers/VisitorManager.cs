using EventService.BL.Exceptions;
using EventService.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventService.BL.Managers
{
    public class VisitorManager
    {
        private Dictionary<int,Visitor> _visitors=new Dictionary<int,Visitor>();
        private int _id = 1;
        public VisitorManager() {
            _visitors.Add(_id, new Visitor("John", _id++, DateTime.Parse("12/3/1979")));
            _visitors.Add(_id, new Visitor("Jane", _id++, DateTime.Parse("1/3/1995")));
            _visitors.Add(_id, new Visitor("David", _id++, DateTime.Parse("12/3/2001")));
            _visitors.Add(_id, new Visitor("Chris", _id++, DateTime.Parse("12/9/1999")));
        }

        public bool ExistsVisitor(int id)
        {
            return _visitors.ContainsKey(id);
        }

        public List<Visitor> GetAllVisitors()
        {
            return _visitors.Values.ToList();
        }

        public Visitor GetVisitor(int id)
        {
            if (_visitors.ContainsKey(id)) return _visitors[id];
            throw new VisitorManagerException("GetVisitor");
        }

        public Visitor RegisterVisitor(Visitor visitor)
        {
            visitor.SetId(_id++);
            return visitor;
        }
        public void SubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("subscribe");
            if (_visitors.ContainsKey(visitor.Id)) throw new VisitorManagerException("subscribe - duplicate");
            _visitors.Add(visitor.Id, visitor);
        }
        public void UnSubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("unsubscribe");
            if (!_visitors.ContainsKey(visitor.Id)) throw new VisitorManagerException("unsubscribe - duplicate");
            _visitors.Remove(visitor.Id);
        }
    }
}
