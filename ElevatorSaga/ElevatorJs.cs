using Microsoft.JSInterop;

namespace ElevatorSaga;

public class ElevatorJs : IElevator
{
    private readonly IJSObjectReference _nativeObject;

    public ElevatorJs(IJSObjectReference nativeObject)
    {
        _nativeObject = nativeObject;
    }


    public static async Task<ElevatorJs> Create(IJSObjectReference nativeObject)
    {
        var elevator = new ElevatorJs(nativeObject);
        await nativeObject.InvokeVoidAsync("setCallback", DotNetObjectReference.Create(elevator));
        return elevator;
    }

    public async Task GoToFloor(int floor)
    {
        await _nativeObject.InvokeVoidAsync("goToFloor", floor);
    }

    public async Task Stop()
    {
        await _nativeObject.InvokeVoidAsync("stop");
    }

    public async Task<int> GetCurrentFloor()
    {
        return await _nativeObject.InvokeAsync<int>("currentFloor");
    }

    public async Task SetGoingUpIndicator(bool value)
    {
        await _nativeObject.InvokeVoidAsync("goingUpIndicator", value);
    }

    public async Task SetGoingDownIndicator(bool value)
    {
        await _nativeObject.InvokeVoidAsync("goingDownIndicator", value);
    }

    public async Task<bool> GetGoingUpIndicator()
    {
        return await _nativeObject.InvokeAsync<bool>("goingUpIndicator");
    }

    public async Task<bool> GetGoingDownIndicator()
    {
        return await _nativeObject.InvokeAsync<bool>("goingDownIndicator");
    }

    public async Task<int> GetMaxPassengerCount()
    {
        return await _nativeObject.InvokeAsync<int>("maxPassengerCount");
    }

    public async Task<double> GetLoadFactor()
    {
        return await _nativeObject.InvokeAsync<double>("loadFactor");
    }

    public async Task<ElevatorDirection> GetCurrentDirection()
    {
        var result = await _nativeObject.InvokeAsync<string>("destinationDirection");
        return Enum.Parse<ElevatorDirection>(result, true);
    }

    public async Task<int[]> GetDestinationQueue()
    {
        return await _nativeObject.InvokeAsync<int[]>("getDestinationQueue");
    }

    public async Task<int[]> GetPressedFloors()
    {
        return await _nativeObject.InvokeAsync<int[]>("getPressedFloors");
    }

    public event Action? Idle;
    public event Action<int>? FloorButtonPressed;
    public event Action<int, ElevatorDirection>? PassingFloor;
    public event Action<int>? StoppedAtFloor;

    [JSInvokable]
    public virtual void OnIdle()
    {
        Idle?.Invoke();
    }

    [JSInvokable]
    public virtual void OnFloorButtonPressed(int obj)
    {
        FloorButtonPressed?.Invoke(obj);
    }

    [JSInvokable]
    public virtual void OnPassingFloor(int arg1, ElevatorDirection arg2)
    {
        PassingFloor?.Invoke(arg1, arg2);
    }

    [JSInvokable]
    public virtual void OnStoppedAtFloor(int obj)
    {
        StoppedAtFloor?.Invoke(obj);
    }
}