namespace ElevatorSagaBlazor
{
    public class GameImplementation : IGameImplementation
    {
        public async Task Initialize(IEnumerable<IFloor> floors, IEnumerable<IElevator> elevator)
        {
            foreach (var floor in floors)
            {
                var f = await floor.GetFloorNumber();
                floor.UpButtonPressed += OnUpButtonPressed;
                floor.DownButtonPressed += OnDownButtonPressed;
            }
        }

        private void OnDownButtonPressed()
        {
            
        }

        private void OnUpButtonPressed()
        {
        }
    }
}
