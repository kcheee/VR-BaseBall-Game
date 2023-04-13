# VR-BaseBallGame
파일 현재태오류 상태

## 하드웨어

### 배트설계

<img src="https://user-images.githubusercontent.com/86779278/227450107-f5520905-edb5-424f-b13c-5caeb32f6edc.png">


![image](https://user-images.githubusercontent.com/86779278/227450451-0f88e59a-e189-4eaa-9378-2f05246e7b74.png)


액추에이터를 제외한 모든 센서는 보드로부터 전력을 공급받고 액추에이터는 배터리에서 직접 공급받습니다.<br>
배터리의 전력 14.8V가 레귤레이터를 거쳐 5V로 변환된 후 보드에 공급되고 액추에이터를 제외한 모든 센서가 보드로부터 5V를 공급받습니다.<br>
액추에이터는 레귤레이터를 거치지 않은 배터리의 전력 14.8V으로 직접 공급받습니다. <br>

### 배트구성

배트는 배트 하우징, NUCLEO-F446RE(보드), 액추에이터, 바이브레이터, 버튼, 블루투스 모듈, AHRS, 레귤레이터, 배터리로 구성되어있습니다.

#### 배트 하우징

![ss](https://user-images.githubusercontent.com/81669996/231773694-dfc82d81-9534-4807-b433-44c264ef2d14.png)

배트 하우징은 배트를 구성하는 여러 부품과 실제 배트 사이즈를 고려하여 직접 모델링해서 3D프린터로 출력하였습니다.

#### NUCLEO-F446RE(보드)

작성한 코드를 보드에 넣어 연결된 모든 센서를 제어합니다.

#### 액추에이터

배터리에서 14.8V의 전력을 직접 공급받습니다. <br>
배트를 3등분 하여 각 부분에 배치했고, 인게임에서 공이 맞은 부분에 따라 데이터가 들어오면 액추에이터가 작동하도록 하여 타격감을 더했습니다.

#### 바이브레이터

바이브레이터는 하나가 부착되어 있기 때문에 각 액추에이터가 작동할 때 마다 작동하도록 하였습니다.<br>
액추에이터가 작동할 때 마다 진동이 울리기 때문에 더욱 현실감 있는 타격감을 만들어 냈습니다.
```c
void act() //각 flag의 상태에 따라 액추에이터,진동모터 
{
    if (flag1 == true)
    {
        IN1 = 0;
        vib = 0;
        flag1 = false;  
    }else if( flag2 ==true)
    {
        IN5 = 0;
        vib = 0;
        flag2 = false;  
    }else if(flag3==true)
    {
        IN3 = 0;
        vib = 0;
        flag3 = false;  
    }
    else
    {   
        IN1 = 1;
        IN3 = 1;
        IN5 = 1;
        vib = 1;
    }       
} 
void read() //값을 읽어오는 함수
{
    if(BLUETOOTH.readable()){
        char c=BLUETOOTH.getc();
        if(c=='1'&&flag1==false) {
            flag1=true;       
        }
        if(c=='2'&&flag2==false) {
            flag2=true;      
        }
        if(c=='3'&&flag3==false) {
            flag3=true;       
        }
    }
}
```
    액추에이터와 바이브레이터 작동 함수입니다.
    블루투스로 전달받은 값에 따라 액추에이터와 진동모터가 작동합니다.
    변수의 값이 바뀌고 액추에이터와 진동모터가 계속 작동하는 것을 방지하기 위해 flag변수를 사용했습니다.

#### 버튼

버튼은 두 개를 연결하여 각 버튼을 누를 시 데이터를 보내 투구와 배트 위치 초기화의 역할을 수행합니다.
```c
void btn_1(){
    if(button1==1&&flag4==false){
        BLUETOOTH.putc('!');
        flag4=true;
        }
        if(button1==0){
            flag4=false;
            }
    }
    
void btn_2(){
    if(button2==1&&flag5==false){
        BLUETOOTH.putc('?');
        flag5=true;
        }
        if(button2==0){
            flag5=false;
            }
    }
 ```
    버튼 작동 함수입니다.
    각 버튼을 누르면 인게임으로 각 데이터를 보내 각자의 역할을 수행합니다.
    마찬가지로 값이 계속 들어가는 것을 방지하기위해 flag변수를 사용했습니다.
    
#### 블루투스 모듈

블루투스 모듈을 연결하여 무선으로 컴퓨터와 연결이 가능하도록 해 인게임과 배트 간에 데이터 입출력을 무선으로 할 수 있도록 했습니다. 

#### AHRS

배트의 각속도 값, 위치 값을 얻기 위한 센서입니다. AHRS를 통해 얻은 데이터를 인게임으로 보내 배트의 움직임을 구현합니다.
```c
int main()
{
    PC.baud(115200);
    IN1=IN3=IN5=vib=1;
    t3.attach(act, 0.1);
    t2.attach(read, 0.1);
    t4.attach(btn_1, 0.1);
    t5.attach(btn_2,0.1);
    
    while(1) {
    if(AHRS.readable()) {
    BLUETOOTH.putc(AHRS.getc());
       }
       }
    }
```
    배트의 끊김없고 부드러운 모션을 위해서 티커를 활용하는 것이 아닌 while문 안에 AHRS의 값을 보내는 코드를 넣었습니다.
    값을 읽고 액추에이터와 센서를 작동시키는 함수와 버튼 작동 는 티커를 활용하여 0.1초에 한 번씩 실행시킵니다.

#### 레귤레이터

연결된 배터리는 14.8V이지만 보드에 필요한 전력은 5V이기 때문에 5V로 변환해 보드로 전력을 공급합니다.

#### 배터리

3.7V배터리 4개를 사용하여 보드 및 액추에이터에 전력을 공급합니다. <br>
배트에 배터리를 내장시켰기 때문에 실제 야구배트와 마찬가지로 자유로운 움직임을 가질 수 있습니다.









## 소프트웨어

### 포트 오픈 
- 들어오는 데이터는 양방향 통신 데이터를 처리하기 위해 스레드를 사용하였습니다.
- ahrs에서 들어오는 데이터를 분리하여 배트의 position, rotation 값에 넣었습니다.   

- 버튼을 눌렀을시 들어오는 데이터 '!', '?'로 설정하고 각 char값이 들어올 때 투수가 공을 던지고, 배트의 위치가 초기화 되도록 설정하였습니다.
- 인게임 상에서 공이 배트에 맞는 위치를 3등분(각 데이터 '1','2','3')하여 그 부분의 액추에이터와 바이브레이터가 켜져 타격감을 주도록 만들었습니다.   
  
	(테스트 영상)   
	

[![Video Label](http://img.youtube.com/vi/lDdn7Zv6igA/0.jpg)](https://youtu.be/lDdn7Zv6igA)

### 야구 배트   

- 배트의 운동에너지를 넣기 위하여 배트를 3등분하여 batfollowerprefab을 만들어서 휘둘렀을 시 각각의 다른 운동에너지가 작용하게 만들었습니다.   

	(테스트 영상)   
	
[![Video Label](http://img.youtube.com/vi/UHFD1X_e1nc/0.jpg)](https://youtu.be/UHFD1X_e1nc)
### 수비
- - -
#### 공 도착지점 예측  

- 프레임을 사용하여 각도와 속도를 계산하여 삼각비 공식을 사용해 공의 도착지점을 예측하였습니다.
```c#
 private Vector3 ConvertAngleToVector(float deg) // 삼각비 이용해 좌표 구하기 (거리와 각도 이용해)  
    {
        var rad = deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad) * Destination, 0, Mathf.Cos(rad) * Destination);    
    }
```

#### 수비수   

 1. 도착지점과 거리가 가장 가까운 수비수의 오브젝트의 followball 스크립트가 켜져 공을 따라감.   
 
 2. 공이 바닥에 닿지않고 공을 잡앗을 시 아웃처리.   
 
 3. 2번이 아니면 아웃 시킬 수 있는 베이스로 공을 던진다.
 
 ![image](https://user-images.githubusercontent.com/86779278/227711746-da3ff810-02a0-47aa-a1d2-d990c808763c.png)

- - -

### 점수 시스템
- 실제 야구룰과 같게 만들었다.

### 7회말이 끝나면 게임이 끝나게 된다.
(사진)

### 구동영상
=======
VR-BaseballGame
>>>>>>> 03baed8d9a1303d677892649656ee8c78ac8dbf7
