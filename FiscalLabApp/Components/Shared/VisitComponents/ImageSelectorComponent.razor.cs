using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class ImageSelectorComponent : ComponentBase
{
    [Parameter]
    public EventCallback<List<Image>> OnSaveButtonClick { get; set; }
    
    [Parameter]
    public IReadOnlyList<Image> Images { get; set; } = [];
    [Parameter]
    public bool IsProcessing { get; set; }
    private List<Image> _images = [];
    
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    private const int MaxAllowedSize = 10 * 1024 * 1024;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _images = Images.ToList();
    }

    private async Task OnChangeHandler(InputFileChangeEventArgs e)
    {
        foreach (var imgFile in e.GetMultipleFiles(20))
        {
            var resizedImg = await imgFile.RequestImageFileAsync(imgFile.ContentType, 600, int.MaxValue);

            var buffers = new byte[resizedImg.Size];
            var amount = await resizedImg
                .OpenReadStream(MaxAllowedSize)
                .ReadAsync(buffers);

            if (amount <= 0) return;
            
            var imgName = resizedImg.Name;
            var image = new Image
            {
                Id = Guid.NewGuid().ToString(),
                Name = imgName
            };
            
            using (var memoryStream = new MemoryStream())
            {
                await resizedImg.OpenReadStream().CopyToAsync(memoryStream);
                image.Data =  memoryStream.ToArray();
            }
            _images.Add(image);
        }
    }

    private async Task OnSaveHandlerAsync()
    {
        await OnSaveButtonClick.InvokeAsync(_images);
    }

    private void OnDeleteButtonClickHandler(string imageName)
    {
        _images = _images
            .Where(x => !x.Name.Equals(imageName))
            .ToList();
    }
    
    private void OnDescriptionChangeHandler(Tuple<string, string> imageData)
    {
        var (name, description) = imageData;
        _images.ForEach(x =>
        {
            if (x.Name.Equals(name))
            {
                x.Description = description;
            }
        });
    }
}