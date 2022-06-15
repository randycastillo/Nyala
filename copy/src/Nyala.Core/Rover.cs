namespace Nyala.Domain
{
    public class Rover : IRover
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public DirectionEnum Direction { get; set; }
        public int GridSize { get; set; }
        public List<(int, int)> Obtacles { get; set; }

        public Rover(int gridSize) : this(0, 0, DirectionEnum.North)
        {
            this.GridSize = gridSize;
        }

        public Rover(int xCoordinate, int yCoordinate, DirectionEnum direction)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
            this.Direction = direction;
        }

        public string GetCurrentCoordinates()
        {
            return $"{this.XCoordinate},{this.YCoordinate} facing {this.Direction}";
        }

        public void Command(string setOfCommands)
        {
            var commands = setOfCommands.Split(",").AsEnumerable();
            foreach (var command in commands)
            {
                switch (command)
                {
                    case "f":
                        this.MoveForward();
                        break;
                    case "b":
                        this.MoveBackward();
                        break;
                    case "l":
                        this.MoveLeft();
                        break;
                    case "r":
                        this.MoveRight();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveForward()
        {
            _ = this.YCoordinate < this.GridSize ? this.YCoordinate++ : this.YCoordinate = 0;
            this.Direction = DirectionEnum.North;
        }

        private void MoveBackward()
        {
            _ = this.YCoordinate == 0 ? this.YCoordinate = this.GridSize : this.YCoordinate--;
            this.Direction = DirectionEnum.South;
        }

        private void MoveRight()
        {
            _ = this.XCoordinate < this.GridSize ? this.XCoordinate++ : this.XCoordinate = 0;
            this.Direction = DirectionEnum.East;
        }

        private void MoveLeft()
        {
            _ = this.XCoordinate == 0 ? this.XCoordinate = this.GridSize : this.XCoordinate--;
            this.Direction = DirectionEnum.West;
        }

        private void DetectObstacles()
        {
            if (!this.Obtacles.Any())
            {
                return;
            }
        }
    }
}
