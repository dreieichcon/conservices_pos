# conservices_pos
A Point of Sales (POS) application for (mainly non-commercial role-playing) events, using pretix.eu as the Ticketshop.

## Features
Which Tickets for which price should be sold is configured via the pretix webshop. All Information is taken via pretix REST API.

- System-Configuration: API Basepoint and API Key
- Tickets: only tickets with the correct ``sales_channel`` should be loaded
- Payment Methods: cash, optional girocard
- KassenSichV compliant, using a TSE
- Reciept Printing via ``ESC/POS``-compliant Printer such as Epson TM-T88
- Touchscreen-Interface, used Screen Resolutions: ``1024x768``, ``1280x1024`` and ``1920x1080`` as these are common touch-screen resolutions
- Pretix Checkin with optional Badge-Printing (Badge-Data from conservices.de Exhibitor-Module)

## Name suggestions
Innkeep (logo see files)


## Ressources
### Pretix API

Documentation: https://docs.pretix.eu/en/latest/

Basepoint for pretix-Servers: https://pretix.eu/api/v1/ 

### Epson ESC/POS Standard
https://reference.epson-biz.com/modules/ref_escpos/index.php?content_id=2

https://learn.microsoft.com/de-de/windows/uwp/devices-sensors/epson-esc-pos-with-formatting

https://github.com/mtmsuhail/ESC-POS-USB-NET

### Card-Payment-Providers

#### Sum Up
https://developer.sumup.com/

Costs:
- 153,51 € Terminal with printer, accepting NFC and chip
- 46,41 € Terminal accepting NFC and chip
- 0,9 % Debit-Card
- 1,9 % Credit-Card

#### iZettle
https://developer.zettle.com/

Costs:
- 34,51 € (first Device) / 94,01 € (other devices) , Zettele Reader 2, accepting NFC and chip
- 0,95 % VPay, Visa Electron, Maestro
- 2,75 % all other cards
- 0,9% PayPal QR-Code



### TSE
#### Fiskaly GmbH Cloud TSE
[Fiskaly Website](https://www.fiskaly.com/)

Using Api: [SIGN DE API V2](https://developer.fiskaly.com/api/kassensichv/v2)

There is a one time cost per activation and a monthly cost while activated, which is low enough for small organizations/clubs.



## Hardware
If possible: 19" Racks for easier transport. Initial Build: 1 Central Module and 1-2 POS Modules
### Central Module

- 19" Rack
- UPS
- TSE Server
- Router
- Switch
- Wireless AP

### POS Modules

- Built inside Rack, with Power In and Network In
- Computer: ThinkCenter Tiny or similar
- Keyboard/Mouse: Logitech K400+ or similar
- Touchscreen: used ELO 19" 4:3 (open frame) or new 16:9 22"
- Thermal Printer: Epson TM-T88 (Mark 3 or newer)
- Cash Drawer
- Customer Display

### optional / future modules:

#### game-management-module
- Rack with TinkCenter Tiny or similar, 1-2 Screens, Keyboard and Mouse
- for easy setup of game-management places

#### 4-Color-Laser-Printer Module

#### Badge-Printer-Module
- Based around Zebra / Entrust (formerly Datacard) or similar direct card printer


## Racks:
Flightcase Manufacturer:
- https://www.sound-labicki.de/
- https://www.amptown-cases.de/en/
- https://www.procaseshop.de/
- https://www.casebuilder.com/
- https://www.thomann.de/de/casefactory.html
- https://www.thomann.de/de/casefactory.html

Flight Case Parts:
- https://www.flightcase-teile.de/
- https://www.aweo.de/Casebau-Material
- https://www.plattenzuschnitt24.de/
