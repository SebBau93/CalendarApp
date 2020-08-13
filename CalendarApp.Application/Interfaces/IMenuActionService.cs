using CalendarApp.Domain.Entities;
using System.Collections.Generic;

namespace CalendarApp.Application.Interfaces
{
    public interface IMenuActionService
    {
        IEnumerable<MenuAction> ReturnMenuActionsByType(int menuActionType);
    }
}
