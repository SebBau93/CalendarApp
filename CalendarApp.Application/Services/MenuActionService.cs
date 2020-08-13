using CalendarApp.Application.Interfaces;
using CalendarApp.Domain.Constants;
using CalendarApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CalendarApp.Application.Services
{
    public class MenuActionService : IMenuActionService
    {
        private readonly List<MenuAction> _menuActions;

        public MenuActionService()
        {
            _menuActions = new List<MenuAction>();
            Initialize();
        }

        public IEnumerable<MenuAction> ReturnMenuActionsByType(int menuActionType)
        {
            return _menuActions.Where(m => m.Type.Equals(menuActionType));
        }

        private void Initialize()
        {
            _menuActions.Add(new MenuAction { Id = 1, Name = "Calendar", Type = MenuActionTypes.Main });
            _menuActions.Add(new MenuAction { Id = 2, Name = "Reminders", Type = MenuActionTypes.Main });
            _menuActions.Add(new MenuAction { Id = 3, Name = "Close application", Type = MenuActionTypes.Main });

            _menuActions.Add(new MenuAction { Id = 1, Name = "Calendar for selected month", Type = MenuActionTypes.Calendar });
            _menuActions.Add(new MenuAction { Id = 2, Name = "Main menu", Type = MenuActionTypes.Calendar });

            _menuActions.Add(new MenuAction { Id = 1, Name = "Reminders for a selected month", Type = MenuActionTypes.Reminder });
            _menuActions.Add(new MenuAction { Id = 2, Name = "Reminders for a selected day", Type = MenuActionTypes.Reminder });
            _menuActions.Add(new MenuAction { Id = 3, Name = "Reminder details", Type = MenuActionTypes.Reminder });
            _menuActions.Add(new MenuAction { Id = 4, Name = "Add new reminder", Type = MenuActionTypes.Reminder });
            _menuActions.Add(new MenuAction { Id = 5, Name = "Delete reminder", Type = MenuActionTypes.Reminder });
            _menuActions.Add(new MenuAction { Id = 6, Name = "Main menu", Type = MenuActionTypes.Reminder });

            _menuActions.Add(new MenuAction { Id = 1, Name = "Calendar", Type = MenuActionTypes.CalendarMonth });
            _menuActions.Add(new MenuAction { Id = 2, Name = "Reminders for a selected month", Type = MenuActionTypes.CalendarMonth });

        }
    }
}
