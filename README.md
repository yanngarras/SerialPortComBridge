# SerialPortComBridge

## Create a bridge between 2 serial com ports 

SerialPortComBridge creates a bridge between 2 serial ports by reading data on each and writing them to the other one.
It allows to load a built-in analyser to decode the exchanged datas. At the time, there is one Analyser for Modbus protocol.

## Why ?

Sometimes, an application communicates with a device via a serial communication port and it's hard to diagnoses the issues. This tool aims to log and eventually decode the bytes exchanged.

## How to use it ?

Because this tool makes a bridge between two ports, if you use an application connected directly to a device on a port, you will need to create 2 virtuals ports. This can be done via the opensource project com0com (https://sourceforge.net/projects/com0com/)

Once it's done, connect the application to one the virtual ports thenyou can launch this tool to bridge the second virtual port and the physical port. 

Here is the worflow : 

Application <=> VirtualPort1 <=> VirtualPort2 <=> SerialPortComBridge <=> PhysicalPort <=> Device


```
SerialPortComBridge.exe COMX-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] COMY-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] [Analyser]

SerialPortComBridge.exe COM1-9600-8-N-2 COM2-115200-7-O-1-DTR-RTS Modbus
```

# SerialPortComBridge

## Create a bridge between 2 serial com ports 

SerialPortComBridge creates a bridge between 2 serial ports by reading data on each and writing them to the other one.
It allows to load a built-in analyser to decode the exchanged datas. At the time, there is one Analyser for Modbus protocol.

## Why ?

Sometimes, an application communicates with a device via a serial communication port and it's hard to diagnoses the issues. This tool aims to log and eventually decode the bytes exchanged.

## How to use it ?

Because this tool makes a bridge between two ports, if you use an application connected directly to a device on a port, you will need to create 2 virtuals ports. This can be done via the opensource project com0com (https://sourceforge.net/projects/com0com/)

Once it's done, connect the application to one the virtual ports thenyou can launch this tool to bridge the second virtual port and the physical port. 

Here is the worflow : 

Application <=> VirtualPort1 <=> VirtualPort2 <=> SerialPortComBridge <=> PhysicalPort <=> Device


```
SerialPortComBridge.exe COMX-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] COMY-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] [Analyser]

SerialPortComBridge.exe COM1-9600-8-N-2 COM2-115200-7-O-1-DTR-RTS Modbus
```

## Result

Here is the output without specifying an analyser, you can see the bytes : 

![Output](https://raw.githubusercontent.com/yanngarras/SerialPortComBridge/main/img/output.png)

When specifying ana analyser, the data will be "decoded" : 
![Output](https://raw.githubusercontent.com/yanngarras/SerialPortComBridge/main/img/output_with_analyser.png)


## Contribute

PR are welcome :-)

Analysers are simple class that need to implement 2 methods. It would be nice to have more analyysers like Async Serial, 1-Wire, ...

## Contribute

PR are welcomes :-)

Analysers are simple class that need to implement 2 methods. It would be nice to have more analyysers like Async Serial, 1-Wire, ...