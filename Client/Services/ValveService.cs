namespace Client.Services
{
    public class ValveService
    {
        public void setValveState(bool state)
        {
            Console.WriteLine("The valve is " +(state?"open":"closed")+"    ip: "+ System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(1).ToString());
            
            /*int pin = 18;
            using var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);
            controller.Write(pin, ((value=="true") ? PinValue.High : PinValue.Low));*/
        }
    }
}
