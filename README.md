# Custom SearchBar and SideMenu
 
 # GIF
<html>
  <table style="width:100%">
    <tr>
      <th>SearchBar</th>
      <th>SideMenuLeft</th> 
      <th>SideMenuRight</th>
    </tr>
    <tr>
      <td><img id="AnimatePlaceholder" src="https://github.com/Mohamadfawzy/CustomSearchBar/blob/main/Screenshots/Gif/searchBar.gif?raw=true"></td>
      <td><img id="PopupWindow" src="https://github.com/Mohamadfawzy/CustomSearchBar/blob/main/Screenshots/Gif/sideMenuLTR.gif?raw=true"> </td>
      <td><img id="FoucsEntry" src="https://github.com/Mohamadfawzy/CustomSearchBar/blob/main/Screenshots/Gif/sideMenuRTL.gif?raw=true"> </td>
    </tr>
  </table>
</html>

Packages used:

* Xamarin.Forms
* Xamarin.Essentials
* Xamarin Community Toolkit 
* Xamarin.Forms.PancakeView

Features:

* BindableProperty
* Animations.
* Embedded Fonts
* TouchEffect.
* Renderers
* MVVM
* Controllers
* languages⁯
* PanGestureRecognizer

## Just use this code to see it anywhere in the app 
``` xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage x:Name="this">
    <Grid x:Name="GrandParent" RowDefinitions="120,*">
        <!-- Items on your page 
        ... -->

        <controllers:SideMenu x:Name="sideMenu"
                            Grid.Row="0" Grid.RowSpan="2"
                            IsPresented="False">
        </controllers:SideMenu>
    </Grid>
</ContentPage>
```
## To open side Menu
``` c#
private void TapOpenSideMenu(object sender, EventArgs e)
{
    sideMenu.IsPresented  = true;
}
```