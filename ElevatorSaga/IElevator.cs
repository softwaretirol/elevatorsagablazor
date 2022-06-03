namespace ElevatorSaga;

public interface IElevator
{
    Task GoToFloor(int floor);
    Task Stop();
    Task<int> GetCurrentFloor();
    Task SetGoingUpIndicator(bool value);
    Task SetGoingDownIndicator(bool value);
    Task<bool> GetGoingUpIndicator();
    Task<bool> GetGoingDownIndicator();
    Task<int> GetMaxPassengerCount();
    Task<double> GetLoadFactor();
    Task<ElevatorDirection> GetCurrentDirection();
    Task<IList<int>> GetDestinationQueue();
    Task<IList<int>> GetPressedFloors();
    Task SetDestinationQueue(int[] floors);


    event Action Idle;
    event Action<int> FloorButtonPressed;
    event Action<int, ElevatorDirection> PassingFloor;
    event Action<int> StoppedAtFloor;

}

public enum ElevatorDirection
{
    Stopped,
    Up,
    Down,
}