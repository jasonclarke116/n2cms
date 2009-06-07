using System;
using System.Collections.Generic;
using N2.Collections;
using N2.Integrity;
using N2.Web.Mvc;

namespace N2.Templates.Mvc.Items.Pages
{
	[Definition("Calendar", "Calendar", "A list of recent events.", "", 120)]
	[RestrictParents(typeof (IStructuralPage))]
	[MvcConventionTemplate("CalendarList")]
	public class Calendar : AbstractContentPage
	{
		public virtual IEnumerable<Event> GetEvents()
		{
			foreach (Event child in GetChildren(new TypeFilter(typeof (Event)), new AccessFilter()))
				yield return child;
		}

		public virtual IList<Event> GetEvents(DateTime day)
		{
			return
				GetChildren(new TypeFilter(typeof (Event)), new AccessFilter(),
				            new DelegateFilter(c => ((Event) c).EventDate.HasValue && ((Event) c).EventDate.Value.Date == day.Date))
					.Cast<Event>();
		}
	}
}