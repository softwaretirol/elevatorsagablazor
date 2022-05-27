using Microsoft.JSInterop;

namespace ElevatorSagaBlazor;

public class GameJavascriptTranslator
{
    private readonly IGameImplementation _gameImplementation;
    private readonly IJSRuntime _jsRuntime;

    public GameJavascriptTranslator(IGameImplementation gameImplementation, IJSRuntime jsRuntime)
    {
        _gameImplementation = gameImplementation;
        _jsRuntime = jsRuntime;
    }

    [JSInvokable("init")]
    public async Task Init(int elevatorCount, int floorCount)
    {
        List<FloorJs> floors = new();
        List<ElevatorJs> elevators = new();

        for (int i = 0; i < elevatorCount; i++)
        {
            var elevator = await _jsRuntime.InvokeAsync<IJSObjectReference>("getElevator", i);
            elevators.Add(await ElevatorJs.Create(elevator));
        }

        for (int i = 0; i < floorCount; i++)
        {
            var floor = await _jsRuntime.InvokeAsync<IJSObjectReference>("getFloor", i);
            floors.Add(await FloorJs.Create(floor));
        }

        await _gameImplementation.Initialize(floors.AsEnumerable(), elevators.AsEnumerable());
    }
}

public interface IFloor
{
    Task<int> GetFloorNumber();
    event Action UpButtonPressed;
    event Action DownButtonPressed;
}

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

public interface IElevator
{
}

public class ElevatorJs : IElevator
{
    private readonly IJSObjectReference _nativeObject;

    public ElevatorJs(IJSObjectReference nativeObject)
    {
        _nativeObject = nativeObject;
    }


    public static async Task<ElevatorJs> Create(IJSObjectReference nativeObject)
    {
        return new ElevatorJs(nativeObject);
    }
}