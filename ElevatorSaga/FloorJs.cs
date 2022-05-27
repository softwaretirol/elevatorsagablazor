using Microsoft.JSInterop;

namespace ElevatorSaga;

public class FloorJs : IFloor
{
    private readonly IJSObjectReference _nativeObject;

    public FloorJs(IJSObjectReference nativeObject)
    {
        _nativeObject = nativeObject;
    }

    public async Task<int> GetFloorNumber()
    {
        return await _nativeObject.InvokeAsync<int>("floorNum");
    }

    public event Action? UpButtonPressed;
    public event Action? DownButtonPressed;

    [JSInvokable]
    public virtual void OnUpButtonPressed()
    {
        UpButtonPressed?.Invoke();
    }

    [JSInvokable]
    public virtual void OnDownButtonPressed()
    {
        DownButtonPressed?.Invoke();
    }

    public static async Task<FloorJs> Create(IJSObjectReference nativeObject)
    {
        var floor = new FloorJs(nativeObject);
        await nativeObject.InvokeVoidAsync("setCallback", DotNetObjectReference.Create(floor));
        return floor;
    }
}