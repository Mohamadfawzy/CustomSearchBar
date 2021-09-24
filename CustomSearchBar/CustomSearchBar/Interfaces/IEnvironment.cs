using System;
using System.Collections.Generic;
using System.Text;

namespace CustomSearchBar.Interfaces
{
    public interface IEnvironment
    {
        void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint);
        void SetNavigationBarColor(System.Drawing.Color color);
        void ShowStatusBar(System.Drawing.Color color);
        void HideStatusBar();

    }
}
