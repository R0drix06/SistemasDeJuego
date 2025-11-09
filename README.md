# Cómo jugar:



##### LEFT/RIGHT ARROWS: MOVE

##### X: JUMP

##### C: DASH

# 

# Patrones utilizados



#### **Singleton**

Lo utilizamos para volver únicos a los managers y globalizarlos, lo que permite utilizar funciones que afectan al macro del juego desde cualquier Script, sin la necesidad de referencias en Awake() o Start().

Los Scripts que lo poseen son IterationManager, para el reseteo y progreso de loops/level, y CustomUpdateManager, para el control de ejecución de Update(), teniendo funciones de registro llamadas en IterationManager. También lo posee GameManager, pero es un Script que aún no se utiliza.



#### **Factory**

La utilizamos para controlar la instanciación del obstáculo Rocket en el juego. La utiliza exclusivamente la Pool.

El Script que llama a la Factory se llama ObstacleFactory.



#### **State**

Lo utilizamos en el Rocket para que, dependiendo de la instancia del trayecto, tenga una velocidad, rotación y consecuencia de colisión distinta.

Al no ser un State que escale más allá de tres estados, el Script que lo posee es el Rocket y los Scripts que implementan IRocketState son: RocketInitState (configuración inicial), RocketMiddleState (configuración media) y RocketFinalState (configuración final + una explosión, que aún falta implementar para el juego final).



#### **Strategy**

Lo utilizamos en varios Scripts como los Commands, los States del Rocket, las implementaciones de IObstacle y las implementaciones de IUpdatable, principalmente para desacoplar el código y también para saber con precisión dónde buscar y qué buscar si hay algún error relacionado al comportamiento de un objeto.

Los Scripts que lo poseen son los que implementan ICommand, IObstacle, IRocketState y IUpdatable.



#### **Flyweight**

Lo utilizamos para minimizar el uso de memoria por medio de Scriptable Objects, que almacenan el estado extrínseco de los Lasers.

Los Lasers poseen un LaserConfig, con la información intrínseca y un LaserTypes, un SO con la información extrínseca.



#### **Pool**

La utilizamos para evitar las múltiples instanciaciones y destrucciones de los Rockets, haciendo que estos oscilen entre estados de activado-desactivado, reiniciando sus datos en cada reactivación y reduciendo el consumo de CPU y del GC.

Se crea en RocketPool, utiliza la ObstacleFactory, se llama en el script RocketLauncher y controla los Rockets.



#### **Type Object**

Lo utilizamos para que el comportamiento de los Lasers recaiga en sus datos, utilizando los SO mencionados en Flyweight como almacenamiento de tipos. Esto nos permitió reducir el obstáculo del láser a un solo Script con comportamiento.

Está en LaserTypes y se utiliza en LaserConfigs



#### **Command**

Lo utilizamos para desacoplar el código del PlayerController, de forma que fuese más sencillo encontrar el comportamiento de los Inputs del jugador.

Se encuentra en los llamados de las acciones del PlayerController, que son implementaciones de ICommand, como DashCommand o JumpCommand.



#### **Facade**

La utiliza el CustomUpdateManager para la ejecución de los Update() de las clases registradas. Permite simplificar toda la ejecución a una línea que no conoce los subsistemas internos ni los modifica, solo los ejecuta.

Se encuentra en IUpdatable el método Tick(). El llamado está en CustomUpdateManager, en su Update(), bajo la línea u.Tick(Time.deltaTime); .









# No añadimos



#### **Abstract Factory**

No aplica al proyecto, hay poca variedad de instanciación, no facilita el código del proyecto y no soluciona problemas a nadie, solo los causa.



#### **Memento**

No logra optimizar nuestro sistema, puesto que es más óptimo hacer LoadScene() para resetear los niveles. El guardado de datos es igual (o más pesado, si está mal implementado) en el impacto en memoria, por lo que no aporta nada al proyecto.



#### **EventQueue**

No descartamos utilizarla a futuro, para niveles más complejos, pero actualmente no podría cumplir su propósito, pues carecemos de secuencias de eventos largas o que requieran el control del tiempo de activación.



#### **Observer**

Un poco relacionado a lo mencionado en EventQueue. No tenemos la necesidad de generar una notificación que llame a múltiples eventos/métodos, porque los sistemas actuales son muy simples y son pocos. Posiblemente también lo implementemos a futuro.

