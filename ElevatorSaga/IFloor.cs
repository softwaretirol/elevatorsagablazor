namespace ElevatorSaga;

public interface IFloor
{
    Task<int> GetFloorNumber();
    event Action UpButtonPressed;
    event Action DownButtonPressed;
}