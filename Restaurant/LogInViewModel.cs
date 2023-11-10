using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class LogInViewModel : ViewModelBase
{
    private string _currentKeyboardLayout;
    private string _capsLockPressed;

    public string CurrentKeyboardLayout
    {
        get { return _currentKeyboardLayout; }
        set
        {
            if (_currentKeyboardLayout != value)
            {
                _currentKeyboardLayout = value;
                OnPropertyChange(nameof(CurrentKeyboardLayout));
            }
        }
    }

    public string CapsLockPressed
    {
        get { return _capsLockPressed; }
        set
        {
            if (_capsLockPressed != value)
            {
                _capsLockPressed = value;
                OnPropertyChange(nameof(CapsLockPressed));
            }
        }
    }
}