
from PIL import Image
import sys
from os import listdir
from os.path import isfile, join

src_path = sys.argv[1]
dest_path = sys.argv[2]
files = [f for f in listdir(src_path) if isfile(join(src_path, f))]

def flip_img(name,s_path,d_path):
    s_img = Image.open(join(s_path,name))
    d_img = s_img.transpose(method=Image.FLIP_LEFT_RIGHT)
    d_img.save(join(d_path,name))
    s_img.close()
    d_img.close()

for name in files:
    if name.endswith(".png"):
        flip_img(name,src_path,dest_path)
