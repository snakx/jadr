import time
import subprocess

print("Create jadr-server.txt")
subprocess.Popen('jadr-server.exe')

time.sleep(5)

print("Push jadr-server.txt")
subprocess.call('adb push "jadr-server.txt" "/data/local/tmp/jadr-server.txt"')
print("Push jadr-agent.apk")
subprocess.call('adb push "jadr-agent.apk" "/data/local/tmp/jadr-agent.apk"')

time.sleep(7)

print("Uninstall jadr-agent.apk")
subprocess.call('adb shell pm uninstall "org.snakx.jadr_agent"')

time.sleep(7)

print("Install jadr-agent.apk")
subprocess.call('adb shell pm install -g "/data/local/tmp/jadr-agent.apk"')

time.sleep(7)

print("Start adb-remote-screen.jar")
subprocess.Popen('java -jar adb-remote-screen.jar')

time.sleep(2)

print("Start jadr.exe")
subprocess.Popen('jadr.exe')