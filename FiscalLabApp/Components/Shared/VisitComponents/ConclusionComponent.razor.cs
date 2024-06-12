using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class ConclusionComponent : ComponentBase
{
    [Parameter]
    public Conclusion Conclusion { get; set; } = new();
    
    [Parameter]
    public Menu[] Menus { get; set; } = [];

    [Parameter]
    public List<Image> Images { get; set; } = [];
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    private const int MaxAllowedSize = 10 * 1024 * 1024;

    private string _modalDisplay = "none;";
    private string _modalClass = string.Empty;
    private bool _showBackdrop;

    private void OpenImages()
    {
        _modalDisplay = "block";
        _modalClass = "show";
        _showBackdrop = true;
    }

    private void CloseImages()
    {
        _modalDisplay = "none";
        _modalClass = string.Empty;
        _showBackdrop = false;
    }
    
    private async Task OnFileSelection(InputFileChangeEventArgs e)
    {
        foreach (var imgFile in e.GetMultipleFiles(20))
        {
            var resizedImg = await imgFile.RequestImageFileAsync(imgFile.ContentType, 600, int.MaxValue);

            var buffers = new byte[resizedImg.Size];
            var amount = await resizedImg
                .OpenReadStream(MaxAllowedSize)
                .ReadAsync(buffers);

            if (amount <= 0) return;
            var imageType = resizedImg.ContentType;
            var imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
            var imgName = resizedImg.Name;

            Images.Add(new Image { Name = imgName, Url = imgUrl });
        }
    }

    private void SaveImages()
    {
        Images = Images;
        CloseImages();
        StateHasChanged();
    }

    private void RemoveImage(string name)
    {
        Images = Images.Where(i => !i.Name.Equals(name)).ToList();

        if (!Images.Any())
        {
            SaveImages();
        }

        StateHasChanged();
    }
    
    private async Task OnEditOptionsButtonClickHandler(Menu menu)
    {
        await OnEditOptionsButtonClick.InvokeAsync(menu);
    }
}