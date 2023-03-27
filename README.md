# VR-BaseBallGame
파일 현재태오류 상태

## 하드웨어

희도야 언제올리니

<img src="https://user-images.githubusercontent.com/86779278/227450107-f5520905-edb5-424f-b13c-5caeb32f6edc.png">


![image](https://user-images.githubusercontent.com/86779278/227450451-0f88e59a-e189-4eaa-9378-2f05246e7b74.png)}


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
