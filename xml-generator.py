import xml.etree.cElementTree as ET
import random

def generateAgents(amount):
    agents = ET.Element("agents")
    for i in range(amount):
        generateAgent(agents,i)
    arbol = ET.ElementTree(agents)
    arbol.write("agents.xml","UTF-8")

def generateName():
    names = ["Adam","Adolfo","Adrián","Alba","Alberto","Alejandra","Alfonso",
             "Alicia","Amanda","Ana","Ángel","Angélica","Antonio","Ariel","Arturo",
             "Bautista", "Belén","Bernardo","Bianca","Bruno","Camila","Carla","Carlos",
             "Carmen","Catalina","Cecilia","César","Cintia","Clara","Claudio","Cristian",
             "Dan", "Daniel","Daniela","Danilo","David","Débora","Diana","Diego","Eduardo",
             "Elisa","Emilio","Emanuel","Enrique","Ernesto","Esteban","Eugenia","Fabián",
             "Fabio","Fabiola","Fátima","Federico","Felipe","Fernando","Fernanada","Francisco",
             "Gabriela","Gabriel","Gerardo","Gonzalo","Guillermo","Gustavo","Héctor","Hernán",
             "Horacio", "Hugo","Humberto","Ignacio","Ingrid","Isabel","Isidro","Iván","Jafet",
             "Javier","Jennifer","Jessica","Jimena","Eva","Joaquín","Jorge","José","Jose","Josué",
             "Juan","Julián","Julia","Julio","Laura","Leonardo","Leticia","Luis","Lorena",
             "Lucía","María","Magdalena","Manuela","Marcela","Marco","Mariana","Mario","Martín",
             "Mateo","Miguel","Mónica","Natalia","Nicolás","Pablo","Patricia","Patricio",
             "Paulo","Rafael","Ramón","Raúl","Ricardo","Rita","Rogelio","Rubén","Sebastián",
             "Sandra","Sara","Selena","Silvia","Sofía","Sonia","Tadeo","Tomás","Valeria","Verónica",
             "Víctor","Walter","Xiomara","Yessenia","Joshua","Albert","Peter",
             "Kevin","Bryan","Byron","Paul","Jason","Christopher","Liam","James",
             "Jessie","Logan","Evelyn","Olivia"]
    lastNames = ["Abarca","Baltodano","Álvarez","Esquivel","Arias","Lizano","Bonilla",
                 "Flores","Calderón","Castillo","Vega","Castro","Garro","Gómez","Granados",
                 "Herrera","Hernández","Jiménez","Alfaro","Monge","Angulo","Montero","Mora",
                 "Murillo","Masís","Solís","Quesada","Calderón","Qurós","Alvarado","Rodríguez",
                 "Rojas","Serrano","Torres","Solano","Mendez","Vargas","Villalobos","Gonzáles",
                 "Acuña","Barrantes","Zúñiga","Vásquez","Sánchez","Zamora","López","Muñoz",
                 "Pérez","Rivas","Salas","Espinoza","Arroyo","Bolaños","Calvo","Campos","Chavez",
                 "Corrales","Díaz","Fernández","Hidalgo","Madrigal","Martínez","Solís","Schmidt",
                 "Solórzano"]
    return random.choice(names) + " " + random.choice(lastNames)

def assignService(amount):
    services = ["ICE","ICG","ILA","RCE","RCG","RLA"]
    assigned = []
    for i in range(amount):
        service = random.choice(services)
        services.remove(service)
        assigned.append(service)
    return tuple(assigned)

def generateAgent(root,id):
    agent = ET.SubElement(root, "agent")
    ID = ET.SubElement(agent, "ID")
    ID.text = str(id)
    name = ET.SubElement(agent, "name")
    name.text = generateName()
    services = ET.SubElement(agent, "services")
    for j in assignService(random.randint(2,4)):
        service = ET.SubElement(services, "service")
        service.text = j

def generateOrders(amount):
    orders = ET.Element("orders")
    for i in range(amount):
        generateOrder(orders,i)
    arbol = ET.ElementTree(orders)
    arbol.write("orders.xml","UTF-8")

def generateOrder(root,id):
    order = ET.SubElement(root, "order")
    ID = ET.SubElement(order, "ID")
    ID.text = str(id)
    client = ET.SubElement(order, "client")
    client.text = generateName()
    service = ET.SubElement(order, "service")
    service.text = assignService(1)[0]    

def main():
    print("Cantidad de nodos archivo a generar")
    print("Ejm: 100 agents/10 orders")
    string = input();
    while string:
        cmd = string.split(" ")
        amount = int(cmd[0])
        if cmd[1] == "agents":
            generateAgents(amount)
        else:
            generateOrders(100)
        print("done")
        string = input();

main()
