import cv2 as cv
import numpy as np

img_name = "MuzzleFlash"
img = cv.imread(img_name + ".png", cv.IMREAD_COLOR)
rows,cols,_ = img.shape
img = cv.cvtColor(img, cv.COLOR_RGB2RGBA)
for i in range(rows):
    for j in range(cols):
        if img[i, j, 0]<=100 or img[i, j, 1] <= 100 or img[i, j, 2] <= 100:
            img[i, j] = np.array([255, 255, 255, 255]) - img[i, j]
            img[i, j, 3] = 255 - int((img[i, j, 0] / 3 + img[i, j, 1] / 3 + img[i, j , 2] / 3))
            img[i, j, 2] *= 2
            
        # elif img[i, j, 0] > 50 and img[i, j, 0] <70 and img[i, j, 0] == img[i, j, 1] and img[i, j, 1] == img[i, j, 2]:
        #     img[i, j] = np.array([255, 255, 255, 0])
cv.imwrite(img_name + "_clean.png", img)