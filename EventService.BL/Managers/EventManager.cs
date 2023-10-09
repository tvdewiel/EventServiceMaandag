using EventService.BL.Exceptions;
using EventService.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.BL.Managers
{
    public class EventManager
    {
        private Dictionary<string,Event> _events=new Dictionary<string,Event>();
        public EventManager() {
            _events.Add("ASP.NET Boot", new Event("ASP.NET Boot", DateTime.Parse("24/10/2022"), "Schoonmeersen", 20));
            _events.Add("Bijscholing async", new Event("Bijscholing async", DateTime.Parse("20/10/2022"), "Schoonmeersen", 20));
            _events.Add("MongoDB", new Event("MongoDB", DateTime.Parse("4/12/2022"), "Mercator", 20));
        }
        public Event GetEvent(string name)
        {
            if (!_events.ContainsKey(name)) throw new EventManagerException("getevent");
            return _events[name];
        }
        public List<Event> GetEventsForLocation(string location)
        {
            return _events.Values.Where(e=>e.Location==location).ToList();
        }
        public List<Event> GetEventsForDate(DateTime dateTime)
        {
            return _events.Values.Where(e => e.Date == dateTime).ToList();
        }

        public void SubscribeVisitor(Visitor visitor, Event ev)
        {
            try
            {
                _events[ev.Name].AddVisitor(visitor);
            }
            catch (Exception ex) { throw new VisitorManagerException(ex.Message, ex); }
        }

        public void UnsubscribeVisitor(Visitor visitor, Event ev)
        {
            try
            {
                _events[ev.Name].RemoveVisitor(visitor);
            }
            catch (Exception ex) { throw new VisitorManagerException(ex.Message, ex); }
        }
    }
}
