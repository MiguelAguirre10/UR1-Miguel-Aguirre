using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoCoMoCoTest
{
    public class LoCoMoCo
    {
        public const byte STOP = 0x7F;
        public const byte FLOAT = 0x0F;
        public const byte FORWARD = 0x6f;
        public const byte BACKWARD = 0x5F;
        SerialPort _serialPort;
        public bool Online { get; private set; }

        public LoCoMoCo() { }

        public LoCoMoCo(String port)
        {
            SetupSerialComms(port);
        }

        public void SetupSerialComms(String port)
        {
            try
            {
                _serialPort = new SerialPort(port);
                _serialPort.BaudRate = 2400;
                _serialPort.DataBits = 8;
                _serialPort.Parity = Parity.None;
                _serialPort.StopBits = StopBits.Two;
                _serialPort.Open();
                Online = true;
            }
            catch
            {
                Online = false;
            }
        }

        public void Move(byte left, byte right)
        {
            try
            {
                if (Online)
                {
                    byte[] buffer = { 0x01, left, right };
                    _serialPort.Write(buffer, 0, 3);
                }
            }
            catch
            {
                Online = false;
            }
        }

        public void Close()
        {
            _serialPort.Close();
        }

    }
}

