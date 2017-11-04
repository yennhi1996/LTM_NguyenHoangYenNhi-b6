using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Bai1_Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            byte[] data = new byte[1024];
            string stringData;
            int recv;
            sock.Connect(ipe);
            Console.WriteLine("Connected to server:");
            recv = sock.Receive(data);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine("Receive: {0}", stringData);

            Console.Write("Nhap so luong phan tu: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] array = new int[n];
            for (int i=0;i<n;i++)
            {
                Console.Write("Nhap pt {0}: ", i);
                array[i] = Convert.ToInt32(Console.ReadLine());

            }
            data = BitConverter.GetBytes(n);
            sock.Send(data, sizeof(int), SocketFlags.None);
            for (int i = 0; i < n; i++)
            {
                data = new byte[10];
                data = BitConverter.GetBytes(array[i]);
                sock.Send(data, sizeof(int), SocketFlags.None);
            }
            data = new byte[1024];
            recv = sock.Receive(data);
            int tong = BitConverter.ToInt32(data, 0);
            Console.WriteLine("Tong: {0}", tong);

            sock.Close();
        }
    }
}
