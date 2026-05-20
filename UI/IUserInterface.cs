namespace GarageBuilder.UI
{
    internal interface IUserInterface
    {
        public string MultipleChoiceMenu(string[] menuOptions, out int chosenIndex, string optionalMessage = "");
        public string MultipleChoiceMenu(string[] menuOptions, string optionalMessage);
        public string InputMenu(string primaryMessage, string optionalMessage = "");
        public string RemoveVehicleMenu(string[] vehicles);
        public (string type, string id, string colour, int weight, string[] additionalAttributes) AddVehicleMenu();
    }
}